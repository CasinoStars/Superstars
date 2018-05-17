import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor() {
        
    }

async RollDices(myDices, selectedDices) {
    return await postAsync(endpoint, myDices, selectedDices);
}

async CreateYamsPlayer(gameid, nbturn, dices, dicesvalue) {
    return await postAsync(endpoint,gameid, nbturn, dices, dicesvalue)
}
    }
export default new YamsApiService();