using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace AuctionPortal.BusinessLayer.DataTransferObjects
{
    public class ProductDTO : DtoBase
    {
        public string Name { get; set; }

        public string ProductImgUrl { get; set; }

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
			return obj is ProductDTO dTO &&
				   Name == dTO.Name &&
				   ProductImgUrl == dTO.ProductImgUrl;
        }

        public override int GetHashCode()
        {
            var hashCode = -879323063;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductImgUrl);
            return hashCode;
        }
    }
}
