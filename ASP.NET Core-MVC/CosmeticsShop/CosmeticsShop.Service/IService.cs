using System;
using System.Collections.Generic;
using System.Text;

namespace CosmeticsShop.Service
{
    public interface IService
    {
        void Create(string name, DateTime birthday, bool isYoungDriver);
    }
}
