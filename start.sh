#!/bin/bash

docker build -f .\src\WebAPI\Dockerfile -t psychologies.api ./
docker build -f .\src\WebApp\Dockerfile -t psychologies.web ./src/WebApp/

docker compose up  -d