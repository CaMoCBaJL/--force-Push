// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MySiteServer.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using MySiteServer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\_Imports.razor"
using MySiteServer.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\Pages\Goods.razor"
using System.Text;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\Pages\Goods.razor"
using System.IO;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/goods")]
    public partial class Goods : IndexBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 99 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\Pages\Goods.razor"
           
        private User user;
        private IEnumerable<Good> goods = new List<Good>();
        private IEnumerable<Producer> producers = new List<Producer>();
        private List<uint> inputValues;
        private List<string> imgFiles;
        bool canIAddTheGoods;
        (uint minPrice, uint maxPrice, List<string> brands) filters;
        uint defaultMaxPrice = 0;
        List<bool> isChecked;

        protected override void OnInitialized()
        {
            goods = repository.GetAllGoods();
            if(defaultMaxPrice == 0)
                goods.ToList().ForEach((good) => { if (good.GoodPrice > defaultMaxPrice) defaultMaxPrice = (uint)good.GoodPrice; });
            producers = repository.GetAllProducers();
            ResetFilters();
            imgFiles = new List<string>();
            canIAddTheGoods = true;
            foreach (var item in new DirectoryInfo("./wwwroot/Images").GetFiles())
            {
                imgFiles.Add(item.Name);
            }
            if (!string.IsNullOrEmpty(Service.userName) && !string.IsNullOrWhiteSpace(Service.password))
                foreach (User u in repository.GetAllUsers())
                    if (u.L0gin == Service.userName && u.Passwrd == Service.password)
                        user = u;
            inputValues = new List<uint>();
            for (int i = 0; i < goods.Count(); i++)
            {
                inputValues.Add(1);
            }
        }

        bool IsChecked(string value)
        {
            return filters.brands.Contains(value);
        }

        private uint ShowGoodAmount(int goodNum)
        {
            if (user.UserCart != null)
            {
                uint result = 0;
                foreach (var item in user.UserCart.Split())
                {
                    string s = goodNum.ToString() + "*";
                    string c;
                    if (item.Contains(s))
                    {
                        c = item.Substring(s.Length, item.Length - s.Length);
                        uint.TryParse(c, out result);
                        return result;
                    }
                }
                return 0;
            }
            else
                return 0;
        }

        private void RemoveGoodFromCart(int goodNum)
        {
            if (!string.IsNullOrEmpty(user.UserCart))
            {
                string s = (goodNum.ToString() + '*').ToString();
                if (user.UserCart.Contains(s))
                {
                    StringBuilder result = new StringBuilder();
                    user.UserCart.Split().Cast<string>().ToList().ForEach(new Action<string>((str) =>
                    {
                        if (str.Contains(s))
                        {
                            int newValue = int.Parse(str.Substring(s.Length, str.Length - s.Length)) - (int)inputValues[goodNum];
                            if (newValue > 0)
                                str = str.Substring(0, s.Length) + newValue.ToString();
                        }

                        result.Append(str + " ");
                    }));
                    user.UserCart = result.Remove(result.Length - 1, 1).ToString();
                    repository.UserInfoChanged(user);
                }
            }
        }

        private void AddGoodToCart(int goodNum)
        {
            if (!string.IsNullOrEmpty(user.UserCart))
            {
                string s = goodNum.ToString() + "*";
                if (user.UserCart.Contains(s))
                {
                    StringBuilder result = new StringBuilder();
                    user.UserCart.Split().Cast<string>().ToList().ForEach(new Action<string>((str) =>
                    {
                        if (str.Contains(s))
                            if (goods.ElementAt(goodNum).GoodStackAmount - inputValues[goodNum] - int.Parse(str.Substring(s.Length, str.Length - s.Length)) >= 0)
                                str = str.Substring(0, s.Length) + (int.Parse(str.Substring(s.Length, str.Length - s.Length)) + inputValues[goodNum]).ToString();
                            else
                                canIAddTheGoods = false;
                        result.Append(str + " ");
                    }));
                    user.UserCart = result.Remove(result.Length - 1, 1).ToString();
                }
                else
                    if (goods.ElementAt(goodNum).GoodStackAmount - inputValues[goodNum] >= 0)
                    user.UserCart = (new StringBuilder(user.UserCart).Append(' ' + goodNum.ToString() + '*' + inputValues[goodNum])).ToString();
                else
                    canIAddTheGoods = false;

            }
            else
                if (goods.ElementAt(goodNum).GoodStackAmount - inputValues[goodNum] >= 0)
                user.UserCart = (goodNum.ToString() + '*' + inputValues[goodNum]).ToString();
            else
                canIAddTheGoods = false;
            repository.UserInfoChanged(user);
        }

        public string ImgNum(int i)
        {
            return i.ToString() + ".jpg";
        }

        void AddOrRemoveBrand(int index, string producerName)
        {
            if (filters.brands.Contains(producerName))
            {
                filters.brands.Remove(producerName);
                isChecked[index] = false;
            }
            else
            {
                filters.brands.Add(producerName);
                isChecked[index] = true;
            } }

        void SetMaxPrice(ChangeEventArgs args)
        {
            uint newMaxValue = uint.Parse(args.Value.ToString());
            if (newMaxValue > 0 && newMaxValue < filters.maxPrice)
                filters.maxPrice = newMaxValue;
        }

        void SetMinPrice(ChangeEventArgs args)
        {
            uint newMinValue = uint.Parse(args.Value.ToString());
            if (newMinValue > 0 && newMinValue < filters.maxPrice)
                filters.minPrice = newMinValue;
        }

        void ResetFilters()
        {
            filters.minPrice = 0;
            filters.maxPrice = defaultMaxPrice;
            filters.brands = new List<string>();
            isChecked = new List<bool>();
            foreach (var item in producers)
            {
                filters.brands.Add(item.ProducerName);
                isChecked.Add(true);
            }
        }
    

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private MySiteServer.Data.Repository.IRepository repository { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
