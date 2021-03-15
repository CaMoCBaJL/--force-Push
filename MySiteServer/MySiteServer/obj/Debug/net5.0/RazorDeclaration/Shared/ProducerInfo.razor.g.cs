// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MySiteServer.Shared
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/prodInfo/{prodId}")]
    public partial class ProducerInfo : IndexBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 52 "C:\Users\Анотон\source\repos\MySiteServer\MySiteServer\Shared\ProducerInfo.razor"
       
    [Parameter] public string prodId { get; set; }
    IEnumerable<Good> goods = new List<Good>();
    IEnumerable<User> users = new List<User>();
    Producer producer;
    List<string> imgFiles;
    List<uint> inputValues;
    User curUser;
    string newProducerName;
    string newProducerInfo;

    protected override void OnInitialized()
    {
        users = repository.GetAllUsers();
        goods = repository.GetAllGoods();
        producer = repository.GetAllProducers().Where(p => p.Id == int.Parse(prodId) + 1).First();
        imgFiles = new List<string>();
        if (!string.IsNullOrEmpty(Service.password) && !string.IsNullOrWhiteSpace(Service.password) && !string.IsNullOrEmpty(Service.userName) && !string.IsNullOrWhiteSpace(Service.userName))
            curUser = repository.GetAllUsers().Where(u => u.L0gin == Service.userName && u.Passwrd == Service.password).First();
        newProducerName = producer.ProducerName;
        newProducerInfo = producer.ProducerInfo;
    }

    void ChangeProducerName()
    {
        if (!string.IsNullOrEmpty(newProducerName) && !string.IsNullOrWhiteSpace(newProducerName))
        {
            producer.ProducerName = newProducerName;
            repository.ProducerInfoChanged(producer);
        }
    }

    void ChangeProducerInfo()
    {
        if (!string.IsNullOrWhiteSpace(newProducerInfo) && !string.IsNullOrEmpty(newProducerInfo))
        {
            producer.ProducerInfo = newProducerInfo;
            repository.ProducerInfoChanged(producer);
        }
    }

    private bool UserExistsAndAdmin()
    {
        if (!string.IsNullOrWhiteSpace(Service.userName) && !string.IsNullOrWhiteSpace(Service.password) &&
            !string.IsNullOrEmpty(Service.userName) && !string.IsNullOrEmpty(Service.password))
            for (int i = 0; i < users.Count(); i++)
            {
                if (users.ElementAt(i).L0gin == Service.userName && users.ElementAt(i).Passwrd == Service.password)
                {
                    curUser = users.ElementAt(i);
                    if(curUser.IsAdmin)
                        return true;
                }
            }
        return false;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Data.Repository.IRepository repository { get; set; }
    }
}
#pragma warning restore 1591
