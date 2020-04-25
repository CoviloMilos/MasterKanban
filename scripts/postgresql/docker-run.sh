#!/bin/bash


echo "Running Postgresql container using postgres image"
docker run -d -p 5432:5432 postgres