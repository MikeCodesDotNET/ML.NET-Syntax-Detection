"use strict";

const path = require("path");
const puppeteer = require("puppeteer");

(async () => {
  const htmlFile = path.resolve("./sample.html");
  const browser = await puppeteer.launch();
  const page = await browser.newPage();
  await page.goto("file://" + htmlFile);
  await page.pdf({ path: "./sample.pdf", format: "Letter" });
  await browser.close();
})();