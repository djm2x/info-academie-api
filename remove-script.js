const fs = require('fs');

function removeFolder(path = `${__dirname}/Migrations`) {
    if (fs.existsSync(path)) {
        fs.rmdirSync(path, { recursive: true });
        console.log(`path : ${path} removed succssfuly`);
    }
}

function removeFile(path = `${__dirname}/db/dev.db`) {
    if (fs.existsSync(path)) {
        fs.unlinkSync(path);
        console.log(`path : ${path} removed succssfuly`);
    }
}


function main() {
    try {
        removeFolder(`${__dirname}/Migrations`);
        removeFile(`${__dirname}/db/dev.db`);
    } catch (e) {
        console.log('>>>>>>>>>>>> remove-script trace begin')
        console.log(e)
        console.log('>>>>>>>>>>>> remove-script trace end')
    }
}
// 
main();
