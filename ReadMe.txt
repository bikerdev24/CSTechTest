Assumptions - A cash card or multiple cards one assumes is linked to an account therefore I have used two main data structures. 
The first being a Cash Card which has an identifier and a pin and the second is an Account linked to a cash card which has a Balance property.

The TransactionService does the job of withdrawing or topping up an Account.

The ATM Service behaves like an ATM interface and calls underlying services.

The repository store used is the MemoryCache object for simplicity.

DI tool used is Autofac and test library is NUnit with Moq for mocking objects.

Log4Net used for logging.

The console runner runs one scenario of multiple withdrawals and top ups running at once to simulate the requirement of the card being virtual
and therefore being used in many places at the same time.
