# build environment
FROM node as builder
ARG REACT_APP_SHA 
ENV REACT_APP_SHA=${REACT_APP_SHA}

RUN mkdir /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY package.json /usr/src/app/package.json
RUN npm install -g -s --no-progress yarn
RUN yarn add  react-scripts -g 
COPY . /usr/src/app 
RUN yarn build

# production environment
FROM nginx:1.13.9-alpine
COPY --from=builder /usr/src/app/build /usr/share/nginx/html

# Fix NGINX config for React
#RUN sed -ie 's/index  index.html index.htm;/try_files $uri $uri\/ \/index.html;/g' /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]