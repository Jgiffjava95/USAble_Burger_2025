The queries for constructing the database, tables, and starting data is located here: USAble_Burger_API\DatabaseConnection\Queries\

I did not create Orders or OrderItems by default as these can easily be created while using the app.

Things I would do if this was a real application:
1. Require user token for account login.
2. Either do all monetary calculations on back-end, or double verify that all totals sent by the client are correct on the back-end.
