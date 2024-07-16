import { check, sleep } from "k6";
import execution from "k6/execution";
import http from "k6/http";
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';


export const options = {
    vus: 50,
    duration: "120s",
    //iterations: 1,
  };

  export function setup() {
    const data = { url: "https://mloantopup-bay-kma4-dev.apps.kma-test-bkk2.krungsri.net" };
    return data;
  }

export default function (data) {
    let params = {
      headers: {
        "Content-Type": "application/json",
        "customerIdMd5": "AA543F32A4114EDEE8460011FD7E5138"
      },
    };
    let list = http.get(`${data.url}/v1/mloan/topup/system-config-info`, params);
    check(list, {
      "status is 200": (r) => r.status === 200,
    });
    if (list.error) {
        execution.test.abort('Aborting test. Application is DOWN');
    }
    sleep(randomIntBetween(3, 10)); 

    let getDraft = http.get(`${data.url}/v1/mloan/topup/draft`, params);
    check(getDraft, {
      "status is 200": (r) => r.status === 200,
    });
    if (getDraft.error) {
        execution.test.abort('Aborting test. Application is DOWN');
    }
    sleep(randomIntBetween(3, 10)); 

    let draftId = getDraft.json().draftId;
    let getDraftById = http.get(`${data.url}/v1/mloan/topup/draft?draftId=${draftId}`, params);
    check(getDraftById, {
      "status is 200": (r) => r.status === 200,
    });
    if (getDraftById.error) {
        execution.test.abort('Aborting test. Application is DOWN');
    }
    sleep(randomIntBetween(3, 10)); 
}