using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer 
{
	public class SingleService
	{

		//This is a simple service implementation 
		// to outline holding application wide state, 
		//and handling an event notification

		public Func<object, StringEventArgs, Task> MyEvent;

		public int CountValue;
		public string userName;
		public string password;

		public Task OnCountValueIncreased(object sender, StringEventArgs e)
		{
			userName = e.UserName;
			password = e.Password;
			MyEvent?.Invoke(sender, e);
			return Task.CompletedTask;
		}

	}

	// This is a simple demo of extending EventArgs 
	//to allow sepcific values to be passed in the args

	public class StringEventArgs : EventArgs
	{
		public string UserName { get; set; }
		public string Password { get; set; } 
	}

	public class IndexBase : OwningComponentBase
    {
		public string status { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public string MessageToPass { get; set; }

		[Inject]
		protected SingleService Service { get; set; }


		//This handler will be called whenever the event defined in MyService 
		//triggers, and will accept EventArgs which can be passed from the event caller
		//Note that this needs to be ASYNC so you can await the 'InvokeAsync' call below
		private async Task OnStatusChangedMethod(object sender, EventArgs e)
		{

			//Verify the EventArgs is the sub-type you expect
			if (e.GetType() == typeof(StringEventArgs))
			{
				//Cast EventArgs to the type you need, then extract value
				UserName = (e as StringEventArgs).UserName;
				Password = (e as StringEventArgs).Password;
			}

			//Invoke StateHasChanged in an async call, helps align the thread with the Synchroniztion context
			await InvokeAsync(() => StateHasChanged());
		}


		//Triggered when the page builds and renders
		protected override void OnInitialized()
		{
			//Unsubsribe once to make sure you only have one event subscription
			//This prevents event propogation, and won't do anything unless you are 
			//already subscribed for some reason
			Service.MyEvent -= OnStatusChangedMethod;

			//Subscribe to the event 
			Service.MyEvent += OnStatusChangedMethod;
		}

		//IMPORTANT - override Dispose and unsubscribe on teardown
		protected override void Dispose(bool disposing)
		{
			//PRevents event propogation and memory leaks
			Service.MyEvent -= OnStatusChangedMethod;
		}


	}
}
