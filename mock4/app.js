const express = require('express');
const app = express();
const port = 3000;
const fs = require('fs');
const { getMock, postMock, MOCKS } = require('./mock');


app.use(express.json());

app.get('/', (req, res) => {
    const ct = req.query.ct || MOCKS.length;
    
    res.json({Result: getMock(ct)});
  });

app.post('/', (req, res) => {
  
  const multiplier = req.body.multiplier || 1;
  const names = req.body.names;

  var result = postMock(multiplier, names)

  res.json({ Result: result });
});

app.listen(port, () => console.log(`Example app listening on port ${port}!`))