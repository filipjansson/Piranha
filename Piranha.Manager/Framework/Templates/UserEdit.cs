﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

using Piranha.Manager.Models;
using Piranha.WebPages;

namespace Piranha.Manager.Templates
{
	/// <summary>
	/// Page template for the user edit view.
	/// </summary>
	[Access(Function="ADMIN_USER", RedirectUrl="~/manager/account")]
	public abstract class UserEdit : Piranha.WebPages.ContentPage<UserEditModel>
	{
		/// <summary>
		/// Executes the page.
		/// </summary>
		protected override void ExecutePage() {
			if (!IsPost) {
				if (UrlData.Count > 0) {
					Page.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
					Model = UserEditModel.GetById(new Guid(UrlData[0])) ;
				} else {
					Page.Title = Piranha.Resources.Settings.EditTitleNewUser ;
					Model = new UserEditModel() ;
				}
			}
		}

		/// <summary>
		/// Saves the given user model.
		/// </summary>
		/// <param name="m">The model</param>
		[HttpPost()]
		public void Save(UserEditModel m) {
			Model = m ;
			Page.Title = Piranha.Resources.Settings.EditTitleExistingUser ;

			if (ModelState.IsValid) {
				if (m.Save())
					this.SuccessMessage(Piranha.Resources.Settings.MessageUserSaved) ;
				else this.ErrorMessage(Piranha.Resources.Settings.MessageUserNotSaved) ;
			}
		}

		/// <summary>
		/// Deletes the model with the given id.
		/// </summary>
		/// <param name="id">The id</param>
		public void Delete(string id) {
			Model = UserEditModel.GetById(new Guid(id)) ;

			if (Model.Delete())
				Response.Redirect("~/manager/user?msg=deleted") ;
			else this.ErrorMessage(Piranha.Resources.Settings.MessageUserNotDeleted) ;
		}
	}
}