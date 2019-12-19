<script>
	import { availableTests, selectTest} from '../store/tests.js';

	export const addTest = n => {
		selectTest(n)
        // console.log("about to add test")
		// availableTests.set(n => (console.log("defined?" , n)));
	}
</script>

<button type="button" class="btn btn-warning" on:click={addTest}>
	Add to Selected Tests
</button>