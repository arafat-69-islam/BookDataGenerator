document.addEventListener('DOMContentLoaded', function () {
    const config = {
        currentPage: 1,
        isLoading: false,
        baseApiUrl: '/api/books',
        requestParams: {
            region: 'en-US',
            seed: 42,
            avgLikes: 0,
            avgReviews: 0,
            page: 1
        }
    };
    const elements = {
        bookTable: document.getElementById('book-table'),
        bookTableBody: document.getElementById('book-table-body'),
        regionSelect: document.getElementById('region-select'),
        seedInput: document.getElementById('seed-input'),
        likesSlider: document.getElementById('likes-slider'),
        likesValue: document.getElementById('likes-value'),
        reviewsInput: document.getElementById('reviews-input'),
        randomSeedBtn: document.getElementById('random-seed-btn'),
        exportCsvBtn: document.getElementById('export-csv-btn'),
        loadingIndicator: document.getElementById('loading-indicator')
    };
    init();
    function init() {
        elements.regionSelect.addEventListener('change', handleRegionChange);
        elements.seedInput.addEventListener('change', handleSeedChange);
        elements.likesSlider.addEventListener('input', handleLikesChange);
        elements.reviewsInput.addEventListener('change', handleReviewsChange);
        elements.randomSeedBtn.addEventListener('click', generateRandomSeed);
        elements.exportCsvBtn.addEventListener('click', exportToCsv);
        window.addEventListener('scroll', handleScroll);
        loadBooks();
    }
    async function loadBooks() {
        if (config.isLoading) return;
        config.isLoading = true;
        showLoading(true);
        try {
            const url = new URL(config.baseApiUrl, window.location.origin);
            Object.entries(config.requestParams).forEach(([key, value]) => {
                url.searchParams.append(key, value);
            });
            const response = await fetch(url);
            if (!response.ok) throw new Error('Failed to load books');
            const books = await response.json();
            renderBooks(books);
            config.currentPage = config.requestParams.page;
        } catch (error) {
            console.error('Error:', error);
            alert('Error loading books: ' + error.message);
        } finally {
            config.isLoading = false;
            showLoading(false);
        }
    }
    async function loadMoreBooks() {
        config.requestParams.page = config.currentPage + 1;
        await loadBooks();
    }
    async function generateRandomSeed() {
        try {
            const response = await fetch('/api/books/random-seed');
            if (!response.ok) throw new Error('Failed to get random seed');

            const { data } = await response.json();
            elements.seedInput.value = data;
            config.requestParams.seed = data;
            resetAndReload();
        } catch (error) {
            console.error('Error:', error);
            alert('Error generating random seed: ' + error.message);
        }
    }
    async function exportToCsv() {
        try {
            const url = new URL('/api/export/csv', window.location.origin);
            Object.entries(config.requestParams).forEach(([key, value]) => {
                url.searchParams.append(key, value);
            });
            url.searchParams.append('pages', config.currentPage);

            window.location.href = url;
        } catch (error) {
            console.error('Error:', error);
            alert('Error exporting to CSV: ' + error.message);
        }
    }
    function renderBooks(books) {
        if (config.requestParams.page === 1) {
            elements.bookTableBody.innerHTML = '';
        }
        books.forEach(book => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${book.index}</td>
                <td>${book.isbn}</td>
                <td>${book.title}</td>
                <td>${book.author}</td>
                <td>${book.publisher}</td>
                <td>${book.likes}</td>
                <td>${book.reviews.length}</td>
                <td>
                    <button class="expand-btn" data-book-id="${book.index}">
                        ${book.reviews.length > 0 ? '▼ Show Reviews' : 'No Reviews'}
                    </button>
                </td>
            `;
            elements.bookTableBody.appendChild(row);
        });
        document.querySelectorAll('.expand-btn').forEach(btn => {
            btn.addEventListener('click', function () {
                const bookId = this.getAttribute('data-book-id');
                toggleReviews(bookId);
            });
        });
    }
    function toggleReviews(bookId) {
        console.log(`Toggling reviews for book ${bookId}`);
    }

    function showLoading(show) {
        elements.loadingIndicator.style.display = show ? 'block' : 'none';
    }
    function handleRegionChange() {
        config.requestParams.region = elements.regionSelect.value;
        resetAndReload();
    }
    function handleSeedChange() {
        config.requestParams.seed = parseInt(elements.seedInput.value) || 42;
        resetAndReload();
    }
    function handleLikesChange() {
        const value = parseFloat(elements.likesSlider.value);
        elements.likesValue.textContent = value.toFixed(1);
        config.requestParams.avgLikes = value;
        resetAndReload();
    }
    function handleReviewsChange() {
        config.requestParams.avgReviews = parseFloat(elements.reviewsInput.value) || 0;
        resetAndReload();
    }
    function handleScroll() {
        const { scrollTop, scrollHeight, clientHeight } = document.documentElement;
        if (scrollTop + clientHeight >= scrollHeight - 500 && !config.isLoading) {
            loadMoreBooks();
        }
    }
    function resetAndReload() {
        config.requestParams.page = 1;
        config.currentPage = 1;
        loadBooks();
    }
    // Custom JS can go here // Custom JS can go here

});