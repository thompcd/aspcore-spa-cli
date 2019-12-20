<Nav {active} />
<svelte:head>
<link href="https://stackpath.bootstrapcdn.com/bootswatch/4.4.1/sketchy/bootstrap.min.css" rel="stylesheet" integrity="sha384-2kOE+STGAkgemIkUbGtoZ8dJLqfvJ/xzRnimSkQN7viOfwRvWseF7lqcuNXmjwrL" crossorigin="anonymous">
</svelte:head>
<main>
	<svelte:component this={Route} {params} />
</main>

<script>
	import Navaid from 'navaid';
	import { onDestroy } from 'svelte';
	import Nav from './Nav.svelte';

	let Route, params, active;
	let uri = location.pathname;
	$: active = uri.split('/')[1] || 'home';

	function draw(m, obj) {
		params = obj || {};
		if (m.preload) {
			m.preload({ params }).then(() => {
				Route = m.default;
			});
		} else {
			Route = m.default;
		}
	}

	function track(obj) {
		uri = obj.state || obj.uri;
		if (window.ga) ga.send('pageview', { dp:uri });
	}

	addEventListener('replacestate', track);
	addEventListener('pushstate', track);
	addEventListener('popstate', track);

	const baseUrl = '';
	const router = Navaid('/')
		.on('/', () => import('../routes/Home.svelte').then(draw))
		.on('/about', () => import('../routes/About.svelte').then(draw))
		.on('/build', () => import('../routes/Build.svelte').then(draw))
		// .on('/blog/:title', obj => import('../routes/Article.svelte').then(m => draw(m, obj)))
		.listen();

	onDestroy(router.unlisten);
</script>

<style>
	main {
		position: relative;
		max-width: 56em;
		background-color: white;
		padding: 2em;
		margin: 0 auto;
		box-sizing: border-box;
	}
	:global(h1, h2, h3, h4, small, p, label){
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
	}

	:global(input[type=text] > *){
    -moz-user-select: text;
    -ms-user-select: text;
    user-select: text;
	z-index: 10;
	pointer-events: none;
	}

</style>
