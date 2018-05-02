<template>
    <div class="container">
 
        <form @submit="onSubmit($event)" method="post">
            <!-- <div class="alert aler-danger" v-if="errors.length > 0">
                <b>Les champs suivants sont invalides : </b>
                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>          -->
 
            <div class="form-group">
                <label class="required">Pseudo : </label>
                <input type="text" v-model="item.Pseudo" class="form-control" required>
            </div>
 
        <div class="form-group">
            <label class="required">Mot de passe : </label>
            <input type="text" v-model="item.Password" class="form-control" required>
            <span asp-validation-for="Password"></span>
        </div>
            <button type="submit" class="btn btn-dark btn-block">S'inscrire</button>
    </form>
</div>
  
</template>
<script>
    import { mapActions } from 'vuex'
    import UserApiService from '../services/UserApiService'

    export default {
        data () {
            return {
                item: {},
                errors: []
            }
        },
        methods: {
            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();
                var errors = [];

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        console.log(this.item);
                        console.log(UserApiService.register(this.item));
                        await this.executeAsyncRequest(() => UserApiService.register(this.item));
                        this.$router.replace('');
                    }
                    catch(error) {
                       console.log(error);
                    }
                }
            }
        }
    }
</script>
 
<style lang="scss">
 
</style>