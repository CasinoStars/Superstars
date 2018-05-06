 class UserApiService {
   constructor() {
        this.bases = {};
        this.authenticatedCallbacks = [];
        this.signedOutCallbacks = [];

        window.addEventListener("message", (e) => this.onMessage(e), false);
    }

    get identity() {
        return SuperStars.getIdentity();
    }

    set identity(i) {
        SuperStars.setIdentity(i);
    }

    get isConnected() {
        return this.identity != null;
    }

    get accessToken() {
        var identity = this.identity;
        return identity ? identity.bearer.access_token : null;
    }

    get pseudo() {
        var identity = this.identity;
        return identity ? identity.pseudo : null;
    }

    onMessage(e) {
        if (!e.origin) return;

        var data = e.data;

        if (data.type == 'authenticated') this.onAuthenticated(data.payload);
        else if (data.type == 'signedOut') this.onSignedOut();
    }

    log(selectedBase) {
        var base = this.bases[selectedBase];
        var popup = window.open("/User/"+selectedBase, "Connexion à All-In", "menubar=no, status=no, scrollbars=no, width=700, height=700");
    }

    onAuthenticated(i) {
        this.identity = i;
        for (var cb of this.authenticatedCallbacks) {
            cb();
        }
    }

    registerAuthenticatedCallback(cb) {
        this.authenticatedCallbacks.push(cb);
    }

    removeAuthenticatedCallback(cb) {
        this.authenticatedCallbacks.splice(this.authenticatedCallbacks.indexOf(cb), 1);
    }

    registerSignedOutCallback(cb) {
        this.signedOutCallbacks.push(cb);
    }

    removeSignedOutCallback(cb) {
        this.signedOutCallbacks.splice(this.signedOutCallbacks.indexOf(cb), 1);
    }

    onSignedOut() {
        this.identity = null;

        for (var cb of this.signedOutCallbacks) {
            cb();
        }
    }
}
export default new UserApiService();