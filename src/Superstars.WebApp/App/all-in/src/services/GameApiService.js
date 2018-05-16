import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(endpoint, gametype);
}
    }

export default new GameApiService();