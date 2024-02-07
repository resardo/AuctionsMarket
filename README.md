# AuctionsMarket
This is a .Net Web Api for an auctions website similar in many ways to ebay wherein a user can post items they have for sale. All new users to the site
start with $1000 which they can use to bid with. Once logged in the home page has a table of all current bid that can accessed leading
to details on a separate page. From the details page users can bid on all auctions up to the amount of their remaining funds. After an
auction ends, it pays out the proper amount to the auction poster.

The process of checking whether an auction should be closed is done by a BackgroundService. This service checks db every two minutes for auctions that have passed their end time and then calls a method in domain which completes the neccessary transactions.

This project wast done with a 3 layer architecture using DDD approach and design patterns like: Repository Pattern, Unit of Work pattern, Dependency Injection patterns.
