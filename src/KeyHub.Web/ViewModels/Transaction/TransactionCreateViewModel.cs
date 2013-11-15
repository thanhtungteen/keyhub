﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KeyHub.Data;

namespace KeyHub.Web.ViewModels.Transaction
{
    /// <summary>
    /// Viewmodel for creating an Transaction
    /// </summary>
    public class TransactionCreateViewModel : RedirectUrlModel
    {
        public TransactionCreateViewModel() : base() { }

        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        /// <param name="transaction">Creating transaction</param>
        /// <param name="skus">List of SKUs to select</param>
        public TransactionCreateViewModel(Model.Transaction transaction, List<Model.SKU> skus)
        {
            Transaction = new TransactionCreateViewItem(transaction);

            SKUList = skus.ToSelectList(x => x.SkuId, x => x.SkuCode);
        }

        /// <summary>
        /// Creating Transaction
        /// </summary>
        public TransactionCreateViewItem Transaction { get; set; }

        /// <summary>
        /// List of SKUs to select
        /// </summary>
        public SelectList SKUList { get; set; }
        
        /// <summary>
        /// Convert back to Transaction instance
        /// </summary>
        /// <param name="original">Original Transaction. If Null a new instance is created.</param>
        /// <returns>Transaction containing viewmodel data </returns>
        public Model.Transaction ToEntity(Model.Transaction original)
        {
            return Transaction.ToEntity(null);
        }

        /// <summary>
        /// Retrieve a list of SKU Guids
        /// </summary>
        /// <returns>List of assigned SKU Guids</returns>
        public IEnumerable<string> GetSelectedSkuGuids()
        {
            return Transaction.SelectedSKUGuids.Select(x => x.ToString()).ToList();
        }
    }

    /// <summary>
    /// TransactionViewModel extension that contains a list of SelectedFeature Guids
    /// </summary>
    public class TransactionCreateViewItem : TransactionViewModel
    {
        public TransactionCreateViewItem() : base() 
        {
            SelectedSKUGuids = new List<Guid>();
        }

        public TransactionCreateViewItem(Model.Transaction transaction)
            : base(transaction)
        {
            SelectedSKUGuids = new List<Guid>(
                (from x in transaction.TransactionItems select x.SkuId).ToList()
                );
        }

        /// <summary>
        /// Guid list of selected skus
        /// </summary>
        public List<Guid> SelectedSKUGuids { get; set; }
    }
}