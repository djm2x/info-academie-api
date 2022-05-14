const fs = require('fs');
const path = require('path');

//moves the $file to $dir2
function main(file, dir2) {
  //gets file name and adds it to dir2
  var f = path.basename(file);
  var dest = path.resolve(dir2, f);

  fs.renameSync(file, dest);
};

//move file1.htm from 'test/' to 'test/dir_1/'
try {
  main(`${__dirname}/wwwroot/a/index.html`, `${__dirname}/wwwroot/`);
} catch (e) {
  console.log(e);
}

