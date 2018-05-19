import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    console.log(gametype);
    return await postAsync(endpoint,gametype);
}


/*async CreateYamsGame(pot) {
    return await postAsync(`${endpoint}/CreateYamsGame`, pot);
}*/
    }

export default new GameApiService();