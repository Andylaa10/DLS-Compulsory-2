FROM node:20.1 as build

WORKDIR /app

EXPOSE 3000

COPY /PatientUI2/package*.json .

RUN npm install

COPY /PatientUI2/. ./

CMD ["npm", "run", "dev"]