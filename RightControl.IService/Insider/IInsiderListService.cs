using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IService.Insider
{
    public interface IInsiderListService : IBaseService<YG_InsiderList>
    {
        IEnumerable<dynamic> GetInsiderList(string InsiderPs);  //根据岗位得到所有该岗位的关联方

        IEnumerable<dynamic> GetAllPost();    //得到所有的关联方岗位
    }
}
