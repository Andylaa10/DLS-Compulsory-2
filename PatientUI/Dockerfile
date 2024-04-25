FROM node:20.1 as build

WORKDIR /app

EXPOSE 3000

COPY /PatientUI/package*.json .

RUN npm install

COPY /PatientUI/. ./

CMD ["npm", "run", "dev"]
