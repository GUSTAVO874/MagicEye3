const jsreport = require('jsreport')();

jsreport.init().then(() => {
  console.log('jsreport server started');
}).catch((e) => {
  console.error(e);
  process.exit(1);
});