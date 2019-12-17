<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests, selectedTests, sortAvailableTests } from '../store/tests.js'
    // import { immerObservable} from './store/store.js';

    // import { selectedTests } from '../store/store.js';

    // export let availableTests = [];
    let params;
    let exportName = "TestList";

    // onMount(() => {
    //     fetch('/api/tests')
    //         .then(response => response.json())
    //         .then(data => {
    //             availableTests = data;
    //         });
    // });

    // let list;
    // $: availableTests = availableTests.map((obj, i) => ({ ...obj, id: i}));
    // $: console.log("lists: ", list);
    // $: {
    //     console.log("availableTests: ", availableTests);
    //     console.log("available test string: ", JSON.stringify(availableTests))
    // }

    // const unsubscribe = selectedTests.subscribe( value => {
    //     list = value;
    // });

    // const toggleFollow = () => {
    //     if (following) {
    //     removeFollower($currentUser.id);
    //     } else {
    //     addFollower($currentUser.id);
    //     }
    //     following = !following;
    // };

    // const sortList = ev => {list = ev.detail};
    const sortList = ev => {
        return sortAvailableTests(ev);
    }


    const handleExport = () => { 
        let list = $selectedTests;
        downloadObjectAsJson(list, exportName);
    };

   function downloadObjectAsJson(exportObj, exportName){
    var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj));
    var downloadAnchorNode = document.createElement('a');
    downloadAnchorNode.setAttribute("href",     dataStr);
    downloadAnchorNode.setAttribute("download", exportName + ".json");
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
  }


</script>

<div>
    <h1>Test Builder</h1>
    <form>
        <button type="button" class="btn btn-secondary" on:click={handleExport}>Create Test List</button>
        <label> File Name</label>
        <input type="text" bind:value={exportName} placeholder="file name"/>
        <small id="emailHelp" class="form-text text-muted">Use this to save files for production.</small>
    </form>

<div class="lists">
    <div class="tests" ref="tests">
        <div class="section">
        <h2>Available Tests</h2>
        <small id="availableTestHelp" class="form-text text-muted">These are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>
        </div>
        <!-- {#each $availableTests as {testName, id}}
        
        <h2>{id}</h2><h1>{testName}</h1>
        {/each} -->
        {#if $availableTests}
            <SortableList 
            list={$availableTests} 
            key="id" 
            on:sort={sortList}
            let:item
            let:index >
                <Test {item} {index}/>
            </SortableList>
        {:else}
            <p><em>Loading...</em></p>
        {/if}
    </div>
        <div>
        <div class="section">
        <h2 class="text-warning">Selected Tests</h2>
        <small id="availableTestHelp" class="form-text text-muted">These are the tests that get saved. Make sure they're unique!</small>
        </div>

        {#if $selectedTests}
            <SortableList 
            list={$selectedTests} 
            key="id" 
            on:sort={sortList}
            let:item
            let:index >
                <Test {item} {index} showAddOption={false}/>
            </SortableList>
        {:else}
            <p style="text-align:center;"><em>No tests selected</em></p>
        {/if}
    </div>
</div>


</div>

<style>
form{
    display:inline-block;
    margin-bottom: 4rem;
}

.lists{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}

.section{
    /* fixed subtext height for alignment, check you don't have text collision if you adjust this */
    height: 5rem; 
    margin-bottom: 1rem;
}
</style>