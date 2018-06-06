import { postAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(model) {
        return await postAsync(endpoint, model);
    }

    async GetFakeBalance(){
        return await getAsync(`${endpoint}/FakeBalance`);
    }

    async GetTrueBalance(){
        return await getAsync(`${endpoint}/TrueBalance`);
    }

    async GetWalletAddress(){
        return await getAsync(`${endpoint}/GetAddress`);
    }
}

export default new WalletApiService();