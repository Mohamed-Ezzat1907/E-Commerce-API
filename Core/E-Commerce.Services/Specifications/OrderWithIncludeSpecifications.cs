using E_Commerce.Domain.Entities.OrderAggregate;

namespace E_Commerce.Services.Specifications
{
    public class OrderWithIncludeSpecifications : BaseSpecifications<Order , Guid>
    {
        // Get Order By Id with Includes ==> Criteria: o => o.Id == id
        public OrderWithIncludeSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        // Get Orders By User Email with Includes ==> Criteria: o => o.UserEmail == userEmail
        public OrderWithIncludeSpecifications(string email) : base(o => o.UserEmail == email) 
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            SetOrderBy(o => o.OrderDate);
        }
    }
}
