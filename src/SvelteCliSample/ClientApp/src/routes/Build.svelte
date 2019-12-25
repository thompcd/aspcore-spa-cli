<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    
    let params;
    let exportName = "TestList";
    
    let selectedTests = $availableTests.filter( i => i.testSelected === true);
    
    $:console.log($availableTests)
    $:console.log("selected tests: ", selectedTests)

    const sortSelectedList = ev => { 
        console.log("sort event", ev.detail)
        selectedTests = (ev.detail) };

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

    function addTest(item) {
        const retVal = selectedTests.find( obj => obj === item)

        if (retVal){
            alert(`${item.testName} is already selected`)
            return
        }

        selectedTests.push(item)
        selectedTests = selectedTests;
    }
    
    function handleRemove(event) {
        console.log("event received", event)
        const objIndex = selectedTests.findIndex( obj => obj.id === +event.detail.id)

        //remove it from the selected list
        if (objIndex > -1) {
            selectedTests.splice(objIndex, 1);
            selectedTests = selectedTests;
        }
    }
    
    function removeTest(item) {
        console.log("event received", item)
        const objIndex = selectedTests.findIndex( obj => obj === item)

        //remove it from the selected list
        if (objIndex > -1) {
            selectedTests.splice(objIndex, 1);
            selectedTests = selectedTests;
        }
	}

    /**
     * Moves an item from one list to another list.
     */
    const move = (source, destination, droppableSource, droppableDestination) => {
        const sourceClone = Array.from(source);
        const destClone = Array.from(destination);
        const [removed] = sourceClone.splice(droppableSource.index, 1);

        destClone.splice(droppableDestination.index, 0, removed);

        const result = {};
        result[droppableSource.droppableId] = sourceClone;
        result[droppableDestination.droppableId] = destClone;

        return result;
    };

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

        {#if $availableTests}
            {#each $availableTests.filter( i => i.testSelected === false ) as item, index}
            <Test {item} {index} collapsible={true}>
                <button slot="actionButton" type="button" class="btn btn-warning" on:click={()=> addTest(item)}>
                Add To Selected Tests
                </button>
            </Test>
            {/each}
        {:else}
            <p><em>Loading...</em></p>
        {/if}
        </div>

    </div>
        <div>
        <div class="">
        <h2 class="text-warning">Selected Tests</h2>
        <small class="form-text text-muted section">These are the tests that get saved.<br>Drag them into the order you want to test them!</small>

        {#if selectedTests.length}
            <SortableList 
            bind:list={selectedTests} 
            key="sortId" 
            on:sort={sortSelectedList}
            let:item
            let:index >
                <Test {item} {index} collapsible={false}>
                    <button slot="actionButton" type="button" class="btn btn-danger" on:click={()=> removeTest(item)}>
                    Remove From Selected Tests
                    </button>
                </Test>
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