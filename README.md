# Documentation Review

## Strengths of the documentation:
- Most of the functionality required for a PoS system is well designed.
- All necessary tables are included to support multiple companies and locations in a PoS SaaS (Software as a Service).
- Table relationships are mostly well-defined.
- Overall, the document is mostly solid for implementing the system with minor improvements and solving inconsistencies
- A lot of sequence diagrams
- Some space was left for creative freedom of UI, because only a few windows were fully defined with wireframes

## Weaknesses of the documentation:
- Missing inventory stock management fields in data model
- No item categories specified
- “1 to 1” and “1 to many” relationships are unspecified
- Currency is overrepeated in many fields(the document assumes that items, discounts and tips in the same order can be applied in different currencies?) A suggestion would be to instead put currency under location 
- Product having a foreign key to Location would make more sense instead of Service to Location(Service points to Product anyway)
- Some API contracts return fields that can’t be calculated and do not exist in data model(Changes made category)

## NOT implemented functionality:
- Stripe
- Split payments
- Discounts
- Tips
- Inventory

## Partially implemented
- Taxes(tax calculation in orders)
- Roles(assigned on login)
- Merchant business management(retrieved items are based on location)
- Item variations(backend only)
- Order refunds(backend only)

## Fully implemented
- Order flow
- Reservation flow(with employee qualification checking)
- Login

## Changes made:
- Added durationMinutes to service entity, which wasn't present in domain model.
- Removed description in GetServices, that came from api contracts, as it wasn't present in domain model and isn't absolutely necessary.
- Created foreign key from Product to Location
- Not used many-to-many relationship between Employee and Location(Employee has locationId, so assumed regular many to 1)
- Added Payments table instead of Charge (Payment entries were mentioned in document, but did not appear in data model) and since we did not implement Stripe integration, that information needed to be stored somewhere
- In Payment APIs implemented one CreatePayment function instead of two that were based on payment method, added method field in PaymentRequest instead
- Did not implement RemoveEmployeeRole from Role APIs, because employee without a role, has no purpose and to change their role we already have a role assign request 
…
