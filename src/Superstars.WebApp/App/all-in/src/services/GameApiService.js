import { postAsync } from "../helpers/apiHelper";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(gametype);
}
    }
export default new GameApiService();