using System;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class CategoryDTO
    {
        public string Name { get; set; }

        public bool HasParent => this.Parent != null;

        public CategoryDTO Parent { get; set; }

        public Guid? ParentId { get; set; }
    }
}
