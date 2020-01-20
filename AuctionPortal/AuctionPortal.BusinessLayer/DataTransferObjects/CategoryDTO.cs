using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class CategoryDTO : DtoBase
    {
        public string Name { get; set; }

        public bool HasParent => this.Parent != null;

        public CategoryDTO Parent { get; set; }

        public Guid? ParentId { get; set; }

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            return obj is CategoryDTO dTO &&
                   Name == dTO.Name &&
                   HasParent == dTO.HasParent &&
                   EqualityComparer<CategoryDTO>.Default.Equals(Parent, dTO.Parent) &&
                   EqualityComparer<Guid?>.Default.Equals(ParentId, dTO.ParentId);
        }

        public override int GetHashCode()
        {
            var hashCode = -736575810;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + HasParent.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<CategoryDTO>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid?>.Default.GetHashCode(ParentId);
            return hashCode;
        }
    }
}
