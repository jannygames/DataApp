#!/bin/sh
password=$(openssl rand -base64 12)
#echo $password
echo "$password" > passwords.txt
