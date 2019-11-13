const fs = require('fs');

const MOCK_FILE_NAMES = [
    'mock0',
    'mock1',
    'mock2',
    'mock3',
    'mock4'
]

const getMockDictionary = function (fileNames) {

    var result = [];

    for (let index = 0; index < fileNames.length; index++) {
        const name = fileNames[index];
        const text = JSON.parse(fs.readFileSync(`./templates/${name}.json`, 'utf8')); 
        result.push({name: name, text: text});
    }

    return result;
}

const KEYED_MOCKS = getMockDictionary(MOCK_FILE_NAMES);
const MOCKS = KEYED_MOCKS.map(e => e.text);

const getMock = function (ct) {

    const loops = (ct / MOCKS.length);
    const mod = (ct % MOCKS.length);

    var result = [];
    for (let index = 0; index < loops; index++) {
        result = result.concat(MOCKS);
    }

    result = result.concat(MOCKS.slice(0, mod));

    return result;
}

const postMock = function (multiplier, namesArray) {

    var intersectArr = KEYED_MOCKS
        .filter(kvp => namesArray.includes(kvp.name))
        .map(kvp => kvp.text);

    var result = [];
    
    for (let index = 1; index < multiplier; index++) {
        result = result.concat(intersectArr);
    }
    return result;
}

module.exports = {
    getMock: getMock,
    postMock: postMock,
    MOCK_FILE_NAMES: MOCK_FILE_NAMES,
    MOCKS: MOCKS

}