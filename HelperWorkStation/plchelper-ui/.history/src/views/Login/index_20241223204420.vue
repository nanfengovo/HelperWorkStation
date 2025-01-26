<template>
    <v-html>
        
	<a href="https://front.codes/" class="logo" target="_blank">
		<img src="https://assets.codepen.io/1462889/fcy.png" alt="">
	</a>

	<div class="section">
		<div class="container">
			<div class="row full-height justify-content-center">
				<div class="col-12 text-center align-self-center py-5">
					<div class="section pb-5 pt-5 pt-sm-2 text-center">
						<h6 class="mb-0 pb-3"><span>Log In </span><span>Sign Up</span></h6>
			<input class="checkbox" type="checkbox" id="reg-log" name="reg-log"/>
			<label for="reg-log"></label>
						<div class="card-3d-wrap mx-auto">
							<div class="card-3d-wrapper">
								<div class="card-front">
									<div class="center-wrap">
										<div class="section text-center">
											<h4 class="mb-4 pb-3">Log In</h4>
											<div class="form-group">
												<input type="email" name="logemail" class="form-style" placeholder="Your Email" id="logemail" autocomplete="off">
												<i class="input-icon uil uil-at"></i>
											</div>	
											<div class="form-group mt-2">
												<input type="password" name="logpass" class="form-style" placeholder="Your Password" id="logpass" autocomplete="off">
												<i class="input-icon uil uil-lock-alt"></i>
											</div>
											<a href="#" class="btn mt-4">submit</a>
                            <p class="mb-0 mt-4 text-center"><a href="#0" class="link">Forgot your password?</a></p>
				</div>
			</div>
			</div>
								<div class="card-back">
									<div class="center-wrap">
										<div class="section text-center">
											<h4 class="mb-4 pb-3">Sign Up</h4>
											<div class="form-group">
												<input type="text" name="logname" class="form-style" placeholder="Your Full Name" id="logname" autocomplete="off">
												<i class="input-icon uil uil-user"></i>
											</div>	
											<div class="form-group mt-2">
												<input type="email" name="logemail" class="form-style" placeholder="Your Email" id="logemail" autocomplete="off">
												<i class="input-icon uil uil-at"></i>
											</div>	
											<div class="form-group mt-2">
												<input type="password" name="logpass" class="form-style" placeholder="Your Password" id="logpass" autocomplete="off">
												<i class="input-icon uil uil-lock-alt"></i>
											</div>
											<a href="#" class="btn mt-4">submit</a>
			</div>
		</div>
		</div>
		</div>
		</div>
</div>
		</div>
	</div>
	</div>
	</div>
    </v-html>
</template>

<style>
/*  
Thanks to: Usama Tahir
https://medium.com/@AmJustSam
*/

    *{
    margin: 0;
    padding: 0;
    }

    html{
        width: 100vw;
        height: 100vh;
    }

    /* CSS which you need for blurred box */
    body{
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    background-position: top;
    background-image:url(http://bit.ly/2gPLxZ4);
    width: 100%;
    height: 100%;
    font-family: Arial, Helvetica;
    letter-spacing: 0.02em;
    font-weight: 400;
    -webkit-font-smoothing: antialiased; 
    }

    .blurred-box{
    position: relative;
    width: 250px;
    height: 350px;
    top: calc(50% - 175px);
    left: calc(50% - 125px);
    background: inherit;
    border-radius: 2px;
    overflow: hidden;
    }

    /* .blurred-box:after{
    content: '';
    width: 300px;
    height: 300px;
    background: inherit; 
    position: absolute;
    left: -25px;
    left position
    right: 0;
    top: -25px;  
    top position 
    bottom: 0;
    box-shadow: inset 0 0 0 200px rgba(255,255,255,0.05);
    filter: blur(10px);
    } */


    /* Form which you dont need */
    .user-login-box{
    position: relative;
    margin-top: 50px;
    text-align: center;
    z-index: 1;
    }
    .user-login-box > *{
    display: inline-block;
    width: 200px;
    }

    .user-icon{
    width: 100px;
    height: 100px;
    position: relative;
    border-radius: 50%;
    background-size: contain;
    background-image: url(https://pbs.twimg.com/profile_images/725321660484583424/ArQ4fM3k.jpg);
    }

    .user-name{
    margin-top: 15px;
    margin-bottom: 15px;
    color: white;
    }

    input.user-password{
    width: 120px;
    height: 18px;
    opacity: 0.4;
    border-radius: 2px;
    padding: 5px 15px;
    border: 0;
    }

</style>