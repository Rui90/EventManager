FROM node:alpine AS eventManagerBuild
WORKDIR /app
COPY . .
RUN npm install && npm run build

# stage 2

FROM nginx:alpine
COPY --from=eventManagerBuild /app/dist/EventManager-App /usr/share/nginx/html
EXPOSE 80