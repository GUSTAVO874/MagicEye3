const jsreport = require('jsreport')();

jsreport.init().then(() => {
  // jsreport is running
}).catch((e) => {
  console.error(e.stack);
  process.exit(1);
});