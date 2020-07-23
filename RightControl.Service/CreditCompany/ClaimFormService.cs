using RightControl.Common;
using RightControl.IService.CreditCompany;
using RightControl.Model;
using RightControl.Model.CreditSearch;
using RightControl.Model.Permissions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace RightControl.Service.CreditCompany
{
    public class ClaimFormService:IClaimFormService
    {
        private readonly IFreeSql fsql;
        public ClaimFormService(IFreeSql freeSql)
        {
            fsql = freeSql;
        }

        public AjaxResult ClaimCreditCompany(int creditCompanyId, string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();

            var department = fsql.Select<DepartmentUserModel>()
                .Where(i => i.UserId == user.Id)
                .Include(i => i.Department)
                .ToOne(i => i.Department);

            var creditCompany = fsql.Select<CreditCompanyModel>().Where(i => i.Id == creditCompanyId).First();

            if(department == null)
            {
                return new AjaxResult { state = ResultType.error.ToString(), message = "当前用户部门为空，不允许认领企业!" };
            }
            if(department.BelongStreet != creditCompany.BelongStreet)
            {
                return new AjaxResult { state = ResultType.error.ToString(), message = "该企业辖区与用户部门所属辖区不同，不允许认领!" };
            }

            if (creditCompany == null || user == null)
            {
                return new AjaxResult { state = ResultType.error.ToString(), message = "获取不到纳税企业或者当前用户为空!" };
            }
            else
            {
                if(creditCompany.Status != 1)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "当前纳税企业不是可认领状态!" };
                }
                var userCount = fsql.Select<ClaimFormModel>()
                    .Where(i => i.UserId == user.Id && i.CreditCompany.Status == 0)
                    .Where(i => i.EndSign == 0)
                    .Count();
                if(userCount >= 10)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "认领企业超过10户，不能再继续认领!" };
                }
                else
                {
                    try
                    {
                        fsql.Transaction(() => {
                            var obj = new ClaimFormModel()
                            {
                                UserId = user.Id,
                                CreditCompanyId = creditCompany.Id,
                                ClaimTime = DateTime.Now
                            };
                            var result = fsql.Insert(obj).ExecuteIdentity();

                            if (result > 0)
                            {
                                var update = fsql.Update<CreditCompanyModel>()
                                .Set(i => i.Status, 2)
                                .Where(i => i.Id == creditCompany.Id)
                                .ExecuteAffrows();
                                if (update == 0)
                                {
                                    throw new Exception();
                                }
                            }
                            else
                            {
                                throw new Exception();
                            }
                        });
                        return new AjaxResult { state = ResultType.success.ToString(), message = "认领成功!" };
                    }
                    catch(Exception e)
                    {
                        return new AjaxResult { state = ResultType.success.ToString(), message = "认领失败，请刷新后重试!" };
                    }
                    
                }
            }
        }

        public List<ClaimFormModel> ClaimHistory(int creditCompanyId)
        {
            var result = fsql.Select<ClaimFormModel>()
                .Where(i => i.CreditCompanyId == creditCompanyId)
                .Include(i => i.User)
                .OrderByDescending(i => i.ClaimTime)
                .ToList();

            return result;
        }

        public List<CreditCompanyModel> GetTaxpayerList(string userName)
        {
            var user = fsql.Select<UserModel>().Where(i => i.UserName == userName).First();

            if(user != null)
            {
                var list = fsql.Select<ClaimFormModel>()
                    .Where(i => i.UserId == user.Id && i.EndSign == 0)
                    .Where(i => i.CreditCompany.Status == 2 || i.CreditCompany.Status == 3)
                    .OrderByDescending(i => i.ClaimTime)
                    .ToList(i => i.CreditCompany);

                return list;
            }
            else
            {
                return new List<CreditCompanyModel>();
            }
        }

        public bool VisitingFeedback(VisitingConfirm obj)
        {
            var claimForm = fsql.Select<ClaimFormModel>()
                .Where(i => i.CreditCompanyId == obj.CreditCompanyId && i.EndSign == 0)
                .Where(i => i.CreditCompany.Status == 2 || i.CreditCompany.Status == 3)
                .First();

            if(claimForm == null)
            {
                return false;
            }

            claimForm.VisitingTime = DateTime.Now;
            claimForm.VisitingFeedback = obj.VisitingFeedback;
            claimForm.Remarks = obj.Remarks;
            claimForm.EndSign = 1;

            try
            {
                fsql.Transaction(() => {
                    var count = fsql.Update<ClaimFormModel>()
                    .SetSource(claimForm)
                    .ExecuteAffrows();

                    if (count == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        var updateCount = fsql.Update<CreditCompanyModel>()
                        .Set(i => i.Status, obj.Confirm)
                        .Where(i => i.Id == obj.CreditCompanyId)
                        .ExecuteAffrows();

                        if(updateCount == 0)
                        {
                            throw new Exception();
                        }

                        if(obj.Confirm == 3)
                        {
                            var item = claimForm;
                            item.ClaimTime = DateTime.Now;
                            item.VisitingTime = null;
                            item.VisitingFeedback = 0;
                            item.Remarks = null;
                            item.ContinueItem = 1;
                            item.EndSign = 0;

                            var insertCount = fsql.Insert(item).ExecuteAffrows();

                            if(insertCount == 0)
                            {
                                throw new Exception();
                            }
                        }

                    }
                });

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
