const express = require('express');
const app = express();
const port = 3000;
const fs = require('fs');

const mocks = [
  JSON.parse(fs.readFileSync('./templates/mock1.json', 'utf8')),
  JSON.parse(fs.readFileSync('./templates/mock2.json', 'utf8')),
  JSON.parse(fs.readFileSync('./templates/mock3.json', 'utf8')),
  JSON.parse(fs.readFileSync('./templates/mock4.json', 'utf8')),
  JSON.parse(fs.readFileSync('./templates/mock5.json', 'utf8'))
];

const buildMock = function(ct) {

    const loops = (ct / mocks.length);
    const mod = (ct % mocks.length);
    var result = [];
    for (let index = 0; index < loops; index++) {
      result = result.concat(mocks);
      
    }
  
    result = result.concat(mocks.slice(0, mod));
  
    return result;
  }

  
app.get('/', (req, res) => {
    const ct = req.query.ct || mocks.length;
    
    res.json({Result: buildMock(ct)});
  });

app.listen(port, () => console.log(`Example app listening on port ${port}!`))