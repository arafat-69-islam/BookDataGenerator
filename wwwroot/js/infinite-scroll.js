export function setupInfiniteScroll(loadMoreCallback) {
    let isLoading = false;
    window.addEventListener('scroll', async function () {
        const { scrollTop, scrollHeight, clientHeight } = document.documentElement;

        if (scrollTop + clientHeight >= scrollHeight - 500 && !isLoading) {
            isLoading = true;
            try {
                await loadMoreCallback();
            } finally {
                isLoading = false;
            }
        }
    });
}