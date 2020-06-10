self.addEventListener('install', function(event) {
  console.log('used to register the service worker')
})

self.addEventListener('fetch', function(event) {
  console.log('used to intercept requests so we can check for the file or data in the cache')
})

self.addEventListener('activate', function(event) {
  console.log('this event triggers when the service worker activates')
})