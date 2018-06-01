import { postAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(pseudo, model) {
        return await postAsync(`${endpoint}/${pseudo}`, model);
    }

    async GetFakeBalance(pseudo){
        return await getAsync(`${endpoint}/${pseudo}/FakeBalance`);
    }
}

export default new WalletApiService();