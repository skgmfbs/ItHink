//https://k6.io/docs/using-k6/environment-variables/
//k6 run -e BASE_URL=https://test-api.k6.io env-var.js
//K6_VUS=10 K6_DURATION=10s k6 run env-var.js
//k6 run -e MY_HOSTNAME=https://test-api.k6.io --duration 10s --vus 10 env-var.js

import http from 'k6/http';

export default function () {

    http.get(`${__ENV.BASE_URL}/public/crocodiles/`);
}