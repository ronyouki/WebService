#!/bin/sh
docker run -e OPEN_API_KEY -e COSMOS_DB_KEY -p 8081:5110 webservice
