# Attempt 2 - no alias no driver bridge - OK

Alias are implicit. Bridge is useless.
The key are aliases
 - http://api1/weatherforecast
 - http://api2/weatherforecast

Localhost doesn't work. Internal port doesn't work.

 - http://localhost:5001/weatherforecast
 - http://localhost:5002/weatherforecast


```yaml
version: "3"
services:
    api1:
        image: api1
        container_name: api1
        build:
            context: ./api1
            dockerfile: Dockerfile
        networks:
            - api
        ports:
            - "5001:80"
    api2:
        image: api2
        container_name: api2
        build:
            context: ./api2
            dockerfile: Dockerfile
        networks:
            - api
        ports:
            - "5002:80"        
networks:
    api:
        name: api
        driver: bridge

```

# Attempt 1 - OK

```yaml
version: "3"
services:
    api1:
        image: api1
        container_name: api1
        build:
            context: ./api1
            dockerfile: Dockerfile
        networks:
            api:
                aliases:
                    - api1
        ports:
            - "5001:80"
        depends_on:
                - api2
    api2:
        image: api2
        container_name: api2
        build:
            context: ./api2
            dockerfile: Dockerfile
        networks:
            api:
                aliases: 
                    - api2
        ports:
            - "5002:80"        
networks:
    api:
        name: api
        driver: bridge

```