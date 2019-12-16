<script>
	import { selectedTests } from '../store/stores.js';

	export const addTest = n => {
        console.log("about to add test")
		selectedTests.set(n => (console.log("defined?" , n)));
	}
</script>

<button type="button" class="btn btn-warning" on:click={addTest}>
	Add to Selected Tests
</button>