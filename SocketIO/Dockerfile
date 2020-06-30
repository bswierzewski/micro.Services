FROM arm32v7/node:slim

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
COPY package.json ./usr/src/app

RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

# Bundle app source
COPY . /usr/src/app

CMD [ "node", "server.js" ]