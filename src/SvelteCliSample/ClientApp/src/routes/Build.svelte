<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    
    let params;
    let exportName = "TestList";
    
    $: selectedTests = $availableTests.filter( i => i.testSelected === true);
    $: unSelectedTests = $availableTests.filter( i => i.testSelected === false);
    $:console.log($availableTests)
    $:console.log("selected tests: ", selectedTests)
    $:console.log("unSelected tests: ", unSelectedTests)

    const sortAvailableTests = ev => { availableTests.set(ev.detail) };
    const sortunSelectedList = ev => { unSelectedTests = (ev.detail) };
    const sortSelectedList = ev => { selectedTests = (ev.detail) };

    const handleExport = () => { 
        downloadObjectAsJson(selectedTests, exportName);
    };

   function downloadObjectAsJson(exportObj, exportName){
    var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, "\t"));
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
        <div class="">
        <h2>Available Tests</h2>
        <small class="form-text text-muted section">These are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>

        {#if unSelectedTests}
            {#each unSelectedTests as item, index}
            <Test {item} {index}/>
            {/each}
            <!-- <SortableList 
            list={unSelectedTests} 
            key="sortId" 
            on:sort={sortunSelectedList}
            let:item
            let:index >
                <Test {item} {index}/>
            </SortableList> -->
        {:else}
            <p><em>Loading...</em></p>
        {/if}
        </div>

    </div>
        <div>
        <div class="">
        <h2 class="text-warning">Selected Tests</h2>
        <small class="form-text text-muted section">These are the tests that get saved.<br>Drag them into the order you want to test them!</small>

        {#if selectedTests}
        <!-- TODO: make hoverable -->
            <SortableList 
            list={selectedTests} 
            key="sortId" 
            on:sort={sortSelectedList}
            let:item
            let:index >
                <Test {item} {index} selected={true}/>
            </SortableList>
        {:else}
            <p style="text-align:center;"><em>No tests selected</em></p>
        {/if}
        </div>
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
    height: 2.2rem; 
    margin-bottom: .8rem;
    text-overflow: '';
    overflow: hidden;
}

small{
    padding-bottom: 2rem;
}
</style>