# stage1 as builder
FROM node:10-alpine

# copy the package.json to install dependencies
COPY package.json package-lock.json ./

# Install the dependencies and make the folder
RUN npm install && mkdir /api && mv ./node_modules ./api

WORKDIR /api

COPY . .

EXPOSE 3000

ENTRYPOINT ["npm", "start"]