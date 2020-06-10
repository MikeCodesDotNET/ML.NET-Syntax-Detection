"use strict";

const puppeteer = require("puppeteer");

(async () => {
  const browser = await puppeteer.launch();
  const page = await browser.newPage();
  await page.goto("https://google.com");
  await page.pdf({ path: "./google.pdf", format: "Letter" });
  await browser.close();
})();