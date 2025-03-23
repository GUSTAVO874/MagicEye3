const jsreport = require('jsreport')();

jsreport.init().then(() => {
  // Running
}).catch((e) => {
  // Error during startup
  console.error(e.stack);
  process.exit(1);
});