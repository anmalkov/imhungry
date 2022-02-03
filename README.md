# imhungry
Are you hungry and living in San Francisco? Then this service is for you!

You can search for the nearby food trucks in San Francisco using RESTful API, CLI (planned) or a web site (planned).

## API

### Where's the code

You can find an API project in [./src/ImHungry.Api/](./src/ImHungry.Api/)

### How to use

You can try it here: https://imhungry.azurewebsites.net/v1/ but please add <latitude,longitude> after the URL.
- Example: https://imhungry.azurewebsites.net/v1/37.77638040110528,-122.42590558289392

By default you will receive 5 neareast food trucks from specified location. If you need to have more or less just add parameter ?count=<number> to your Url.
- Example: https://imhungry.azurewebsites.net/v1/37.77638040110528,-122.42590558289392?count=2
